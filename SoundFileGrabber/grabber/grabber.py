from bs4 import BeautifulSoup
import requests
from grabber.results import GrabResults, GrabError
import lxml


class Grabber:
    successful: bool

    def __init__(self, word_to_grab):
        self.word = word_to_grab
        self.successful = None
        self.results = None

        self.__url = 'https://dictionary.cambridge.org/dictionary/english/' + word_to_grab
        self.__header = {
            'User-Agent': 'Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, like Gecko) '
                          'Chrome/88.0.4324.96 Safari/537.36 Edg/88.0.705.50'
        }

    def grab(self):
        try:
            request = requests.get(url=self.__url, headers=self.__header)
            response = request.content
        except requests.exceptions.RequestException as e:
            self.results = GrabError('REQUEST_FAILURE', str(e))
            self.successful = False
            return

        if request.status_code == 302 or request.url == 'https://dictionary.cambridge.org/dictionary/english/':
            self.results = GrabError('FAILURE_NOT_FOUND', 'The vocabulary could not be found.')
            self.successful = False
            return

        self.__internal_grab(response)

    def __internal_grab(self, response):
        # Parse web page here
        bs = BeautifulSoup(response, 'lxml')
        dictionary = bs.select('div.pr.dictionary')[0].contents[1]
        self.results = GrabResults(self.word)
        for entry in dictionary.select('div.pr.entry-body__el'):
            self.__add_to_results(entry)

    def __add_to_results(self, html):
        part_of_speech = html.select('div.pos-header.dpos-h span.pos.dpos')[0].text
        uk_url = 'https://dictionary.cambridge.org' +\
                 html.select('div.pos-header.dpos-h source[type="audio/mpeg"]')[0].attrs['src']
        us_url = 'https://dictionary.cambridge.org' +\
                 html.select('div.pos-header.dpos-h source[type="audio/mpeg"]')[1].attrs['src']
        uk_phonetic = '/' + html.select('div.pr.dictionary div.pr.entry-body__el div.pos-header.dpos-h '
                                        'span.pron.dpron span')[0].text + '/ '
        us_phonetic = '/' + html.select('div.pr.dictionary div.pr.entry-body__el div.pos-header.dpos-h '
                                        'span.pron.dpron span')[1].text + '/ '

        # checks for duplicate
        # if self.results.url_is_exist(uk_url) or self.results.url_is_exist(us_url):
        #     return

        self.results.add_new_data([part_of_speech, uk_url, uk_phonetic, us_url, us_phonetic])

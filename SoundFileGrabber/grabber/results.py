# This file includes Results class.
from typing import List
import json


class GrabResults:

    def __init__(self, word):
        self.word = word
        self.grabbed_data = []

    def add_new_data(self, args: List[str]):
        data_entry = self.GrabbedData()
        data_entry.part_of_speech = args[0]
        data_entry.uk_audio_url = args[1]
        data_entry.uk_phonetic = args[2]
        data_entry.us_audio_url = args[3]
        data_entry.us_phonetic = args[4]

        self.grabbed_data.append(data_entry)

    def to_json(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=False, indent=0).replace('\n', '')

    def url_is_exist(self, url):
        for entry in self.grabbed_data:
            if entry.uk_audio_url == url or entry.us_audio_url == url:
                return True
        return False

    class GrabbedData:

        def __init__(self):
            self.part_of_speech = None
            self.uk_audio_url = None
            self.uk_phonetic = None
            self.us_audio_url = None
            self.us_phonetic = None

        def to_json(self):
            return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True, indent=0).replace('\n', '')


class GrabError:

    def __init__(self, error_code, error_message):
        self.error_code = error_code
        self.error_message = error_message

    def to_json(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=False, indent=0).replace('\n', '')

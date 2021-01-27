from utils.command_line_options import *
import sys
import json
from grabber.grabber import *
from grabber.results import *


def grab_data(options):
    grabber = Grabber(options.word)
    grabber.grab()
    results = grabber.results

    # Writes data to json file
    filename = options.output_dir + 'enquiry_results_' + options.word + '.json'
    with open(filename, 'w', encoding='utf8') as file:
        file.write(results.to_json().encode().decode('unicode-escape'))


if __name__ == '__main__':
    # PRINTS TOOL VERSION #
    print('SoundFileGrabber v0.1')

    options = CommandLineOptions(sys.argv)
    if not options.success:
        exit(1)  # 1 represents commandline option incorrect
    if options.action == Actions.Search:
        grab_data(options)

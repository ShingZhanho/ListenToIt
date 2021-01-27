from utils.command_line_options import *
import sys
import json
from grabber.grabber import *
from grabber.results import *


def grab_data(options):
    grabber = Grabber(options.word)
    grabber.grab()
    results = grabber.results
    if isinstance(results, GrabError):
        print(results.error_code)
    elif isinstance(results, GrabResults):
        print(json.dumps(results.to_json()))


if __name__ == '__main__':
    options = CommandLineOptions(sys.argv)
    if not options.success:
        exit(1)  # 1 represents commandline option incorrect
    if options.action == Actions.Search:
        grab_data(options)

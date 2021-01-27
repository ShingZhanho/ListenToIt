from utils.command_line_options import *
import sys


if __name__ == '__main__':
    options = CommandLineOptions(sys.argv)
    if not options.success:
        exit(1)  # 1 represents commandline option incorrect

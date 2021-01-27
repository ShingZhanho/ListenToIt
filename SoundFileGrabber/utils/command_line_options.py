class CommandLineOptions:
    """Class for processing command line options"""
    success: bool
    word: str

    def __init__(self, args):
        self.success = False
        self.word = ''  # Vocabulary to search in dictionary

        for i in range(0, len(args)-1):
            status = self.__process_argument(args[i+1])
            if status != 0:
                self.success = False
                return

        self.success = True

    def __process_argument(self, arg):
        try:
            command = str(arg).replace("--", "").split(":")[0].lower()
            paras = str(arg).replace("--", "").split(":")[1].split(";")
        except:
            self.success = False
            return 1    # 1 means the program needs to be stopped immediately

        if command == "search":  # Search command
            self.__search_command(paras[0])

        return 0

    def __search_command(self, word):
        self.word = word

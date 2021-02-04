# Listen to It
A really simple tool for listening a word's pronounciation.

## Introduction
This application is built with WinForm, but will soon be rebuilt with WPF.

## Contribution
First of all, thank you for being willing to contribute to this project!! :sparkling_heart:
Please read the following instructions before modifying code.

### Project structure
You may want to learn about the structure of this project in order to contribute:
- `ListenToIt.Core` Core features (UI, Update etc.) are developed here.
  - `ListenToIt.UI` The main UI and features of ListenToIt are developed here.
  - `ListenToIt.Updater` The tool for downloading and installing new updates. The updater is developed separately in branch `dev/updater`. Changes to the dev branch will be merged into `master` after tests.
  - `ListenToIt.Runner` The executable file compiled in this project will be the executable for users to use. This project handles the UI and Updater.
- `SoundFileGrabber` This project is written in Python. The SoundFileGrabber will grab the specified word's pronunciation URLs from [Cambridge Dictionary](dictionary.cambridge.org)
- `More...` More parts will be added as ListenToIt develops.

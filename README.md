# Create .NET .resx translations using Google translate based on a .resx template file.

Create language translation resource files based on a .resx template file. The translation is done manually by using the google translate file feature at https://translate.google.com/.

1. Thus, this script reads a .resx translation file as template and stores each value in a line and saves it as .txt file for google translate. 
2. Then manually translate the text file to your desired languages using Google translate.
3. The script then reads the translation outputs and creates new .resx translation files based on the translation and the initial reference.
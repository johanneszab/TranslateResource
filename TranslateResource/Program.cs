using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace TranslateResource
{
    class Program
    {
        static string resourcesTemplate =
            @"C:\Users\xen\Documents\Visual Studio 2015\Projects\TumblThree\src\TumblThree\TumblThree.Presentation\Properties\Resources.resx";
        static string translationTemplate =
            @"C:\Users\xen\Documents\Visual Studio 2015\Projects\TumblThree\src\TumblThree\TumblThree.Presentation\Properties\Translations\Resources_translate.txt";
        static string translationsDirectory =
            @"C:\Users\xen\Documents\Visual Studio 2015\Projects\TumblThree\src\TumblThree\TumblThree.Presentation\Properties\Translations\";

        static void Main(string[] args)
        {
            // Step 1: Generate template for google translation.
            GenerateTemplateFromResource();

            // Step 2: Manually translate the template to your desired languages.
            // go to https://translate.google.com/, click on "Type text or a website address or translate a document." and translate the Resources_translate.txt
            // Save the outputs to Resources_translated_XX.txt in the same directory whereas XX is the two letter ISO 639-1 (language) code.
            //

            // Step 3: Create .resx files based on the saved translations.
            GenerateTranslations();
        }

        /// <summary>
        /// Generates translation template for google translates (https://translate.google.com/) file translation.
        /// </summary>
        private static void GenerateTemplateFromResource()
        {
            var rsxr = new ResXResourceReader(resourcesTemplate);
            var contents = new List<string>();

            foreach (DictionaryEntry d in rsxr)
            {
                contents.Add(d.Value.ToString().Replace(System.Environment.NewLine, ""));
            }
            System.IO.File.WriteAllLines(translationTemplate, contents);
        }

        /// <summary>
        /// Creates Resources.XX.resx files based on the google translation text file (one line for key-value pair) and the (english) Resource.resx reference.
        /// </summary>
        private static void GenerateTranslations()
        {
            var rsxr = new ResXResourceReader(resourcesTemplate);
            string[] translations = Directory.GetFiles(translationsDirectory, "Resources_translated*.txt", SearchOption.TopDirectoryOnly);

            foreach (var file in translations)
            {
                string letterCode = Path.GetFileNameWithoutExtension(file).Split('_')[2];
                var rsxw = new ResXResourceWriter(Path.Combine(translationsDirectory, "Resources." + letterCode + ".resx"));
                string[] translation = System.IO.File.ReadAllLines(file);

                IDictionaryEnumerator resource = rsxr.GetEnumerator();
                IEnumerator line = translation.GetEnumerator();
                {
                    while (resource.MoveNext() && line.MoveNext())
                    {
                        rsxw.AddResource(resource.Key.ToString(), line.Current);
                    }
                }
                rsxw.Close();
                rsxr.Close();
                rsxw.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace GsonReader
{
    public class Gson
    {
        public Dictionary<string, string> subCategory = new Dictionary<string, string>();
        public Gson(string message)
        {
            int indentlevel = 0;
            int mainIndex = 0;
            string identifier = "";
            string content = "";
            bool writingIdentifier = false;
            bool secondterm = false;

            foreach(char c in message)
            {
                if (c == '{')
                    indentlevel++;
                else if (c == '}')
                {
                    if (indentlevel < 0)
                        throw new Exception("Invalid Json format");
                    indentlevel--;
                }
                if(indentlevel == 1)
                {
                    if(c == '"')
                    {
                        if(identifier == "")
                        {
                            writingIdentifier = true;
                        }
                        else
                        {
                            writingIdentifier = false;
                        }
                    }
                    else if (writingIdentifier)
                    {
                        identifier += c;
                    }
                    else           //!writingIdentifier
                    {
                        if (secondterm)
                        {
                            if(c != ',')
                            {
                                content += c;
                            }
                            else
                            {
                                content = content.Trim();
                                if(content[0] == '"')
                                {
                                    string s = "";
                                    for (int i = 1; i < content.Length - 1; i++)
                                    {
                                        s += content[i];
                                    }
                                    content = s;
                                }

                                subCategory.Add(identifier, content);
                                writingIdentifier = false;
                                secondterm = false;
                                identifier = "";
                                content = "";
                                mainIndex++;
                            }
                        }

                        if (c == ':')
                            secondterm = true;

                    }

                }
                else
                {
                    content += c;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;

[XmlRoot]
public class User {

    [XmlAttribute]
    public string Username = "";
    [XmlAttribute]
    public string AutoLogin = "";
    [XmlElement]
    public List<string> Favorites = new List<string>();
    [XmlElement]
    public List<string> ConnectedAccounts = new List<string>();
    [XmlElement]
    public string AppIdNumber = "";
   
    internal User Load()
    {
        string fileName = Application.persistentDataPath + @"/CurrentUser.xml";
        User obj = null;
        XmlSerializer serializer = new XmlSerializer(typeof(User));
        if (File.Exists(fileName))
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                obj = serializer.Deserialize(stream) as User;
            }   
        }
        else
        {
            Debug.Log("File does not exist: " + fileName);
        }
        return obj;
    }
    public void Save()
    {
        XmlSerializer xmls = new XmlSerializer(typeof(User));
        using (FileStream stream = new FileStream(Application.persistentDataPath + @"/CurrentUser.xml", FileMode.Create))
        {
            xmls.Serialize(stream, this);
        }
    }
    internal static User StringToUser(string text)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text);

            User obj = new User();
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            XmlReader reader = new XmlNodeReader(doc);

            obj = serializer.Deserialize(reader) as User;

            return obj;
        }
        catch (Exception)
        {
            return null;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System;

[XmlRoot]
public class User {

    [XmlAttribute]
    public string Username;
    [XmlAttribute]
    public string AutoLogin;
    [XmlElement]
    public List<string> Favorites;
    [XmlElement]
    public List<string> ConnectedAccounts;
    [XmlElement]
    public string AppIdNumber;
   
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

}

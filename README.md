# XmlSerializer

This is a very simple package for easily saving and loading objects to XML with a couple of static methods. To save an object, use

    XmlSerializerHelper.Save(@object, "{some filename}");
    
To load an object, use

    var x = XmlSerializerHelper.Load<T>("{some filename}");

Install via nuget package **AoXmlSerializer**.

# XmlSerializer Library

Install via nuget package **AoXmlSerializer**.

I know XML is a bit old school compared with Json, but I still really like the XML serialization features built into .NET. This library offers some static helpers and a base class for making serialization very straightforward. There are three things this package offers:

- the `XmlSerializationHelper` static class for easy saving and loading of any objet that supports XML serialization.

- the `Document` abstract class, which implements INotifyPropertyChanged, serves as a base class for objects that you want to save as XML as well as track property changes with the PropertyChanged event.

- the `UserSettings` abstract class, a more simplified version of my [AoOptions](https://github.com/adamosoftware/AoOptions) project. I wanted to remove the WinForms dependency, and offer an overall simpler syntax and approach. See [UserSettings](https://github.com/adamosoftware/XmlSerializer/blob/master/XmlSerializerHelper/UserSettings.cs). Unlike the AoOptions project, encrypted properties, form position tracking, and MRU lists are not built-in.

The XmlSerializationHelper static class has just two methods. To save an object to XML, use

    XmlSerializerHelper.Save(@object, "{some filename}");
    
To load an object from a filename, use

    var @object = XmlSerializerHelper.Load<T>("{some filename}");

To use the `Document` class effectively, use the protected `Set` and `Get` methods to trigger the PropertyChanged event and to ensure that the `IsModified` property is always updated. For example

    public class MyDocument : Document
    {
        public string FirstName { get { return Get<string>(); } set { Set(value); } }
        public string LastName { get { return Get<string>(); } set { Set(value); } }
    }

You can use the static `Load` method to read an object from a file as well as the `Save` (and `SaveAs`) method to write to a file. When saving a `Document` object, you usually need to prompt the user for a filename by displaying the Save File dialog. I didn't want a dependency on WinForms or WPF, so I added a delegate to let you implement your own dialog logic via the `FilenamePrompt` property. For example (in WinForms):

    var doc = new MyDocument();
    doc.FilenamePrompt = delegate (out string fileName)
    {
        fileName = null;
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.DefaultExt = "xml";
        dlg.Filter = "XML Files|*.xml|All Files|*.*";
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            fileName = dlg.FileName;
            return true;
        }
        return false;
    };
    
Setting the FilenamePrompt in this way lets you call the Save method without worrying about whether you need to display the Save File dialog or not. The dialog will appear if the Filename property hasn't been set yet for the document object.

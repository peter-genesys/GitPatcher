Imports Shell32

Public Class zip


    Shared Sub zip_dir(ByVal i_zip_file As String,
                       ByVal i_zip_dir As String)
 

        Dim l_empty_zip_file As New System.IO.StreamWriter(i_zip_file)
        l_empty_zip_file.Write("PK" & Chr(5) & Chr(6) & StrDup(18, vbNullChar))
        l_empty_zip_file.Close()

 
        ''1) Lets create an empty Zip File .
        ''The following data represents an empty zip file.

        'Dim startBuffer() As Byte = {80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, _
        '                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        ''Data for an empty zip file.

        'FileSystem.WriteAllBytes("d:\empty.zip", startBuffer, False)

        'We have successfully made the empty zip file.

        '2) Use the Shell32 to zip your files.
        'Declare new shell class
        Dim sc As New Shell32.Shell()
        'Declare the folder which contains the files you want to zip.
        Dim input As Shell32.Folder = sc.NameSpace(i_zip_dir)
        'Declare  your created empty zip file as folder .
        Dim output As Shell32.Folder = sc.NameSpace(i_zip_file)
        'Copy the files into the empty zip file using the CopyHere command.
        output.CopyHere(input.Items, 4)

    End Sub

    Sub UnZip()
        Dim sc As New Shell32.Shell()
        'Create directory in which you will unzip your files.
        IO.Directory.CreateDirectory("D:\extractedFiles")
        'Declare the folder where the files will be extracted.
        Dim output As Shell32.Folder = sc.NameSpace("D:\extractedFiles")
        'Declare your input zip file as folder.
        Dim input As Shell32.Folder = sc.NameSpace("d:\myzip.zip")
        'Extract the files from the zip file using the CopyHere command.
        output.CopyHere(input.Items, 4)

    End Sub

End Class


# Email Sender Test with FluentEmail
This is a console app that sends email with FluentEmail with a bunch of PDF files attached.

# My Sources
I was watching a video from Tim Corey. He does not mentioned anything about adding attachments which I needed to do. First of all, here is a link to his youtube video. Go give a like and come back!
https://www.youtube.com/watch?v=qSeO9886nRM

# Two ways to add attachments in FluentEmail

#### 1. Attaching the file by file name

```c#
var email = await Email
    .From("john@email.com")
    .To("bob@email.com", "bob")
    .Subject("hows it going bob")
    .AttachFromFilename("C:\\Your Filename.pdf")
    .Body("yo bob, long time no see!")
    .SendAsync();
```

The obvious drawback to this method is that you can only attach a single file per email. I think... I don't see how I could add multiple files from this as there is no overload to this method.

#### 2. Attach a FileStream... 

   How on earth do I do that...? BTW... not just file stream..., any stream should do... I guess. The following is how I managed to do... not near perfect since I have no idea when the file stream gets closed.

   ```c#
    DirectoryInfo d = new DirectoryInfo(@"C:\Export"); //Assuming Test is your Folder
    FileInfo[] Files = d.GetFiles("*.pdf"); //Getting pdf files

   List<Attachment> attachments = new List<Attachment>();
   
   // I guess you are going to make sure that the size of attachments does not 
   // exceed 25 MB or something. The limit.
   
   //may be zip them all up and attach
   var attachmentSize = 0L;
   foreach (var file in Files)
   {
       attachmentSize += file.Length;
       attachments.Add(new Attachment()
                       {
                           ContentId = Guid.NewGuid().ToString(),
                           // if text files use somthing like : text/plain 
                           ContentType = "application/pdf", 
                           Filename = file.Name,
                           IsInline = false,
                           // I wonder when the file stream gets closed!
                           Data = new FileStream(file.FullName, FileMode.Open) 
                       });
   }
   
   //assuming that the limit is 25MB. Does't really matter unless you try to send a real one.
   var attachmentSizeInMB = attachmentSize / (1024 * 1024);
   Console.WriteLine($"Attachment size is {attachmentSizeInMB} MB.");
   if ( attachmentSizeInMB < 25)
   {
       var email = await Email
           .From("john@email.com")
           .To("bob@email.com", "bob")
           .Subject("hows it going bob")
           .Attach(attachments)
           .Body("yo bob, long time no see!")
           .SendAsync();
   }
   else
   {
       Console.WriteLine("Attachment size exceeded 25MB");
   }
   ```

![email](https://github.com/ibrahimhuycn/EmailSenderTest/blob/master/email.PNG)
   


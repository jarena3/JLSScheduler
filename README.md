# JLS Homework Scheduling Utility

##Planned Features:
+ *Refactoring*, which it really needs
+ Better exception handling
+ Formatting options for exported files, custom headers, etc.
+ LPT scraping to automatically generate files for your classes (if I can get access)
+ More thorough code documentation


##Known Issues:
+ If the lpt.gojls excel file is poorly formatted, the program could load the student list with junk data. Make sure the student names list is in the second column and has a header of "학생"
+ Older versions of Microsoft Word can't use the experimental features (export to .doc, .pdf, or .html) and will cause the program to crash. Once I find a way around this, I'll move those out of "Experimental".
+ Some systems may produce garbage data in the Students file that prevents it from opening. This seems to be a System CultureInfo problem. Currently being worked on.
 

##Nerd Stuff:
+ Uses [Cathal Coffey's DocX](https://docx.codeplex.com/) to write word files
+ [LinqToExcel](https://code.google.com/p/linqtoexcel/) to rip data from horrendously-formatted LPT spreadsheets
+ [JSON](http://james.newtonking.com/json) for day/class/time/book configurations

# JLS Homework Scheduling Utility
This is now a standalone app (no more installer!)
Stick in on a USB and bring it to classes. Nice.


##Planned Features:
+ LPT scraping to automatically generate files for your classes (if I can get access)
+ Targeting really old OS versions, for branches with out-of-date classroom computers.

##Known Issues:
+ If the lpt.gojls excel file is poorly formatted, the program could load the student list with junk data. Make sure the student names list is in the second column and has a header of "학생"
+ Older versions of Microsoft Word can't use the experimental features (export to .doc, .pdf, or .html) and will cause the program to crash. Once I find a way around this, I'll move those out of "Experimental".


##Nerd Stuff:
+ Uses [Cathal Coffey's DocX](https://docx.codeplex.com/) to write word files
+ [LinqToExcel](https://code.google.com/p/linqtoexcel/) to rip data from horrendously-formatted LPT spreadsheets
+ [JSON](http://james.newtonking.com/json) for day/class/time/book configurations

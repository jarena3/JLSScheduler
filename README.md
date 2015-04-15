# JLS Homework Scheduling Utility

**Use:**<br>


<br>
**Features:**<br>
You put in class information<br>
You get out these Word (or PDF) files:
+ A class syllabus, student list, and homework tracker for yourself
+ Personalized homework sheets for each student, with...
+ A syllabus for Mom and Dad, complete with Korean introduction
+ A homework list for the class' KT


*Other stuff:*
+ Import from LPT excel class lists for your students' Korean names
+ Save and load classes to edit, work on them later, or send to other teachers
+ Add your own levels, books, and class times through JSON (tutorial [here](#link))
+ Automatically detects and adjusts for Korean and JLS holidays
+ Automatically add weekly speaking tree assignments (reading, listening, etc.)
+ Automatically generate homework entries for presentation preparation
+ Automatically add review days at the end of the semester
+ Add custom homework assignments as single or repeating entries


<br><br>
**Planned Features:**
+ Better exception handling
+ Formatting options for exported files, custom headers, etc.
+ LPT scraping to automatically generate files for your classes (if I can get access)
+ More thorough code documentation


<br><br>
**Known Issues:**<br>
+ If the lpt.gojls excel file is poorly formatted, the program could load the student list with junk data. Make sure the student names list is in the second column and has a header of "학생"
+ Older versions of Microsoft Word can't use the experimental features (export to .doc, .pdf, or .html) and will cause the program to crash. Once I find a way around this, I'll move those out of "Experimental".
+ Some systems may produce garbage data in the Students file that prevents it from opening. This seems to be a System CultureInfo problem. Currently being worked on.

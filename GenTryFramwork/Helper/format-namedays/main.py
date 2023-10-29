#Import helper libraries
import calendar, time
ls = []
with open("text.txt", "+r", encoding= "utf-8") as file:
    #Remove lines that
    ls = file.readlines()
    for i,l in enumerate(ls):
        ls[i] = l.strip()
    #ls is an iterable whose members are only date lines (only lines which have a "." will be preserved)
    ls = filter(lambda x : "." in x ,ls)


finallist = []
finallist.extend(ls)

# the dates
for i, x in enumerate(finallist):
    finallist[i] = x.split("   ")[1].strip()    

#if next year is leap year, leave in the placeholder dot for february 29.
if not calendar.isleap(time.localtime().tm_year+1):
    finallist.remove(".")

#write out remaining lines
with open("names.txt", "w+", encoding="utf-8") as file:
    for line in finallist:
        file.write(line+"\n")
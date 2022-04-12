# MultiFilter
One filter that every WPF application should have.

Do you recognise the following scene:
Someone asks you to adapt your application and add a new filter to it, and the first thing you think about is where am i going to put that?
There is not much space left anymore...

With this filter you can easily build a wpf application and use as many filters you need and still have enough space on your window for other things.

![Screenshot](https://github.com/CanneytWesley/MultiFilter/blob/master/MultiFilter.GUITests/Screenshots/Screenshot2.jpg?raw=true)

## The big advantages
* Less code to maintain
* More space in your window for other stuff
* Very Easy to code
* There's a build in filter to query the result
* More filter options than you usually have
  
 ## 3 kind of filters
 * MultipleChoiceFilter: you use a list of items, pick one out of them and use it to filter
 * ActionFilter: you build a set of items and link an action to each item. Like build a report or open a window.
 * LogicalFilter: you use logic to filter on items: < > != ... Example: Age >50 or windo*
 * BooleanFilter: There are yes/no filters
 
## Implementation

You can find everything in the source code.
There is an example project named GUITests.

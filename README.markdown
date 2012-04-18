# Really Simple A/B Testing

This is a basic example of how to set up an A/B test for an ASP.NET MVC4 application

If you dont know what A/B testing is check out the [Wikipedia Article on A/B testing](http://en.wikipedia.org/wiki/A/B_testing)

Having read [The Lean Start Up](http://theleanstartup.com/) I decided it was about time to get my head around A/B testing and to try and bake it into some of the projects I work on.

I evaluated a number of excellent third party products however they all seemed to target the marketeer persona, who wants to test the change of some copy/banners/images on their site.

I was looking for something that we could build into our application so that content/functions etc could be surfaced to the user in an A/B test, these are the results of my findings.



You can break down an A/B testing into 4 steps

1. Segment - Divide your uses into separate groups for analysis
2. Augment - Change the compositon of your application based on the users segment profile
3. Report - Track your users behaviour
4. Analyse - Determine if you test was successful

## Segment

The first part of a test requires that we segment our user base into a control group and a test group. The proportion of segmentation is only important in that you need enough traffic into the test group to make a __confident__ judgement
The example in this project uses a simple 50/50 segmentation, however this algorithim could be changed to create a smaller test group.
The way that we are going to segment the traffic initially is with a session cookie. This cookie is then used in both the Augmentation and Reporting steps.

This example uses a simple 50/50 "flip a coin" to decide to give a user a "test" cookie during the BeginRequest event of the Global.asax file

### Augment

Everything I build uses (IoC)[http://en.wikipedia.org/wiki/Inversion_of_control] container to manage the composition of an components. Therefore it is a natural extension for me to lean on my IoC container to _Augment_ the application based on a users test group

My (IoC of choice is the excellent Castle Windsor)[http://stw.castleproject.org/Windsor.MainPage.ashx], which had a natural extension point called the IHandlerSelector that you can use to select a particular component based on a give critieria.

Both (Ayende Rahien)[http://ayende.com/blog/3633/windsor-ihandlerselector] and (Mike Hadlow)[http://mikehadlow.blogspot.co.uk/2008/11/multi-tenancy-part-2-components-and.html] have excellent articles talking about how you can use the IHandlerSelector interface so I wont go into detail about how to use it here.

In the example we are going to use it to select a given _IAdvert_ based on the _cookie_ that was set up during the Segmentation phase. However this methodology could be extended to select controllers, databases etc, just about anything that you can think of and has the added benefit of nothaving to pollute your code with "if this then that" statements 


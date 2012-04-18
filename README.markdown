# Really Simple A/B Testing

This is a basic example of how to set up an A/B test for an ASP.NET MVC4 application

If you dont know what A/B testing is check out the [Wikipedia Article on A/B testing](http://en.wikipedia.org/wiki/A/B_testing)

Having read [The Lean Start Up](http://theleanstartup.com/) I decided it was about time to get my head around A/B testing and to try and bake it into some of the projects I work on.

I evaluated a number of excellent third party products however they all seemed to target the marketeer persona, who wants to test the change of some copy/banners/images on their site.

I was looking for something that we could build into our application so that content/functions etc could be surfaced to the user in an A/B test, these are the results of my findings.



You can break down an A/B testing into 4 steps

1. Segment
2. Augment
3. Report
4. Analyse

## Segment

The first part of out test requires that we segment or user base into a control group and a test group. The proportion of segmentation is only important in that you need enough traffic into your test group to make a confident judgement
The example in this project uses a simple 50/50 segmentation, however this algorithim could be changed to create a smaller test group.
The way that we are going to segment the traffic initially is with a session cookie. This cookie is then used in both the Augmentation and Reporting steps.
 


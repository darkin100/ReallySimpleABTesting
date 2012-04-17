$(document).ready(function () {

    //if test cookie exists set custom google user variable with value from test
    var test_name = 'test_test'; //we need to pull this from some configuration source
    
    var cookie = $.cookies.get('test_test');
    if (cookie){
        _gaq.push(['_setCustomVar',
          1,                        // This custom var is set to slot #1.  Required parameter.
          test_name,                // The name acts as a kind of category for the user activity.  Required parameter.
          cookie,                   // This value of the custom variable.  Required parameter.
          2                         // Sets the scope to session-level.  Optional parameter.
       ]);
    }
});
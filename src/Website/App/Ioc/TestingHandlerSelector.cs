using System;
using System.Web;
using System.Linq;
using Castle.MicroKernel;
using Website.Models;

namespace Website.App.Ioc
{
    public class TestingHandlerSelector : IHandlerSelector
    {
        private readonly Func<HttpContext> _context;
        private readonly string _testname;
        

        public TestingHandlerSelector(Func<HttpContext> context,string testname)
        {
            _context = context;
            _testname = testname;
        }

        public bool HasOpinionAbout(string key, Type service)
        {
            return service == typeof(IAdvert);
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            try
            {
                var context = _context.Invoke();

                if (context == null) return null;

                var httpCookie = context.Request.Cookies[_testname];

                if (httpCookie == null) return null;

                return handlers.Where(x => x.ComponentModel.Name.Contains(httpCookie.Value)).FirstOrDefault();                    
                
            }catch(HttpException)
            {
                return null;
            }
        }
    }
}
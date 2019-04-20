# Wedding Planner App

I wrote this with the goal of helping my fiancée and I organize our wedding, though the scope eventually narrowed to a simple app to provide information about our wedding and allow our invitees to easily RSVP paperlessly. All the infrastructure is there to eventually make it more than a simple SPA, but I'll likely split it into separate .NET API and Node.js front-end projects before I get the juicier parts fully implemented and hooked up.

I knew plenty about .NET Core before starting this, but basically nothing about React, Redux, or [Webpack](https://github.com/webpack/webpack). There are a number of crazy things I've figured out along the way. I chose to configure Webpack and [Babel](https://babeljs.io/) manually for React over the [create-react-app](https://github.com/facebook/create-react-app) starter app because I have a general disdain for magic black boxes, and I enjoy retaining control. Plus it helps me better understand what's going on with everything.

The backend is written in C# using [.NET Core 2.2](https://dotnet.microsoft.com/download). The frontend is written in ES7 using [React](https://github.com/facebook/react) + [Redux](https://github.com/reduxjs/redux). I'm using [reactstrap](https://github.com/reactstrap/reactstrap) to style the UI because I generally [suck at front-end design](https://i.redd.it/u9v1bvq0j6611.jpg), though I've gotten a bit better over the life of this project.

There are no tests because of my initial resistance to TDD and due to time constraints. I've made a point to eventually put them into the code, as I imagine this app will continue to grow beyond our wedding date in the hopes that others may find it similarly useful. I also don't want to keep paying for RSVPify, which is currently being injected into the RSVP page via iframe.

This project references my C# [toolbox](https://github.com/jdmallen/toolbox), which is where I store all my handy abstractions, extensions, utility classes, etc., that I've picked up through my career. Much of the toolbox was born out of this project.

## Server Requirements

* Any Windows or Unix-like machine that can run the .NET Core binaries below.
* [.NET Core 2.2 Runtime or SDK](https://dotnet.microsoft.com/download)
* [Node.js & npm](https://nodejs.org/en/)
* _Optional:_ [Yarn](https://yarnpkg.com/en/)*

_*If you choose to use npm instead of Yarn in a non-development environment, you'll need to modify the pre-build targets in the `WeddingPlanner.Web.csproj` file, which are currently set to yarn. It's just faster for me on Windows still, though I've read that [npm is way faster now](https://iamturns.com/yarn-vs-npm-2018/) than it used to be._

## Get it running

1. Clone this repo and `cd` into the directory.
2. Execute `dotnet run -p WeddingPlanner.Web/`
3. In another terminal, execute `yarn watch` or `npm run watch` from `WeddingPlanner.Web/`.
4. Navigate to http://localhost:5000/*

_*I [originally](https://github.com/jdmallen/wedding-planner-app/blob/477c06fa8fd22f536f002fbe116c75830f209ef6/WeddingPlanner.Web/Program.cs?ts=2#L19) configured Kestrel to [bind to SSL ports with a certificate file (or Windows store cert)](https://github.com/jdmallen/toolbox/blob/master/JDMallen.Toolbox/Extensions/StartupExtensions.cs?ts=2#L30-L128), but it was far too great a hassle. I ended up using nginx as a reverse proxy and configuring SSL there instead, which I found far more convenient. [This is also the prescribed method of hosting .NET Core apps by Microsoft as of 2019-03-23](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?tabs=aspnetcore2x&view=aspnetcore-2.2)._

## Special thanks

Thanks to the following references for inspiration and code snippets in designing this:

* [Jason Watmore's React + Redux - User Registration and Login Tutorial & Example](http://jasonwatmore.com/post/2017/09/16/react-redux-user-registration-and-login-tutorial-example)
* [Configuring HTTPS in ASP.NET Core across different platforms](https://blogs.msdn.microsoft.com/webdev/2017/11/29/configuring-https-in-asp-net-core-across-different-platforms/)
* [Token Authentication in ASP.NET Core 2.0 - A Complete Guide](https://developer.okta.com/blog/2018/03/23/token-authentication-aspnetcore-complete-guide)
* [ReactJS + Redux Basics by Academind](https://www.youtube.com/playlist?list=PL55RiY5tL51rrC3sh8qLiYHqUV3twEYU_)
* [Ducks: Redux Reducer Bundles](https://github.com/erikras/ducks-modular-redux)
* [Visual Studio 2017 - Resolving SSL/TLS connections problems with IIS Express by A.J. Saulsberry](https://www.pluralsight.com/guides/visual-studio-2017-resolving-ssl-tls-connections-problems-with-iis-express)

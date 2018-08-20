# Wedding Planner App

I wrote this to help my fiancée and I organize our wedding, and eventually to allow our invitees to easily RSVP paperlessly.

It's written in C# and Javascript using [.NET Core 2.1](https://www.microsoft.com/net/download) and [React](https://github.com/facebook/react) + [Redux](https://github.com/reduxjs/redux). I'm using [reactstrap](https://github.com/reactstrap/reactstrap) to style the UI because I [suck at front-end design](https://i.redd.it/u9v1bvq0j6611.jpg). I knew plenty about .NET Core before starting this, but basically nothing about React, Redux, or [Webpack](https://github.com/webpack/webpack).

There are no tests because I'm lazy and I'm resistant to TDD. I'll eventually put them in, I'm sure (and other jokes I tell myself).

This references my C# [toolbox](https://github.com/jdmallen/toolbox), which will eventually be a Nuget package, but for now just needs to be cloned ajacent to this repo.

## Requirements
* [.NET Core 2.1 Runtime/SDK](https://www.microsoft.com/net/download)
* [My C# toolbox](https://github.com/jdmallen/toolbox) (clone to same directory as this repo)
* [Yarn](https://yarnpkg.com/en/) or [npm](https://nodejs.org/en/)

## Get it running
1. Clone this repo and the [toolbox](https://github.com/jdmallen/toolbox) repo to the same directory.
2. To make the site Secure, add a self-signed cert to the repo and reference it under `Endpoints` in `appsettings.json` via the file name and cert password, then trust it in your browser. Alternatively, if running Windows, add it to your Personal certificate store (as CurrentUser, _not_ LocalMachine, unless you're running your IDE with admin privileges) via MMC. Also add it to the Trusted Root Certificate Authority store so your browser doesn't yell at you.
3. Run `dotnet run` from `wedding-planner/`.
4. Run `yarn watch` or `npm run watch` from `wedding-planner/WeddingPlanner.Web/`.
5. Navigate to https://localhost:44321/

## Special thanks

Thanks to the following references for inspiration and code snippets in designing this:

* [Jason Watmore's React + Redux - User Registration and Login Tutorial & Example](http://jasonwatmore.com/post/2017/09/16/react-redux-user-registration-and-login-tutorial-example)
* [Configuring HTTPS in ASP.NET Core across different platforms](https://blogs.msdn.microsoft.com/webdev/2017/11/29/configuring-https-in-asp-net-core-across-different-platforms/)
* [Token Authentication in ASP.NET Core 2.0 - A Complete Guide](https://developer.okta.com/blog/2018/03/23/token-authentication-aspnetcore-complete-guide)
* [ReactJS + Redux Basics by Academind](https://www.youtube.com/playlist?list=PL55RiY5tL51rrC3sh8qLiYHqUV3twEYU_)
* [Ducks: Redux Reducer Bundles](https://github.com/erikras/ducks-modular-redux)
* [Visual Studio 2017 - Resolving SSL/TLS connections problems with IIS Express by A.J. Saulsberry](https://www.pluralsight.com/guides/visual-studio-2017-resolving-ssl-tls-connections-problems-with-iis-express)

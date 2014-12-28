<h1>LinkedIn APIs for .NET</h1>

LinkedIn APIs for .NET is a robust and clean library that allows a developer to easily make LinkedIn API calls in Microsoft .NET application. The library is built on top of the Owin framework which means integration with LinkedIn external login provider comes out of the box. In other words, logging with LinkedIn works the same way as it works with the default login providers like Facebook, Google, and Twitter.

The current version of the library provides the following functionality:
<ul>
<li>Seamless integration with ASP.NET identity management.</li>
<li>Retrieve basic profile of a LinkedIn user.</li>
<li>Retrieve full profile of a LinkedIn user.</li>
</ul>

<h2>Visual Studio Project Support</h2>
You can use LinkedIn APIs for .NET either in ASP.NET Web Forms or in ASP.NET MVC project. However, the library as of now has been tested only in ASP.NET MVC 5 with .NET Framework 4.5.

<h2>Registering Your LinkedIn App</h2>
You must have unique set of API Key and Secret Key to make the API calls. In order to get the API Key and the Secret Key, you need to register a LinkedIn app using your own LinkedIn account. Or someone else can register the LinkedIn app for you and then share the details with you.

https://www.linkedin.com/secure/developer?newapp=

Ensure that you enter http://rootpath/signin-linkedin in the OAuth 2.0 Redirect URLs field where rootpath is the root path of your web application. Typically it would be your localhost during development and later on the actual public domain once you publish your web application. Example value for OAuth 2.0 Redirect URLs could be: http://localhost:18991/signin-linkedin. Keep in mind that you would be able to make API calls only from the application whose root path matches with the root path specified in the OAuth 2.0 Redirect URLs field. Of course, you can always change the value whenever you need to.
Once you are done with registering your LinkedIn app, you will get your API Key and Secret Key.

<h2>Installing LinkedIn APIs for .NET Package in Your Project</h2>

The best and the recommended way to install the latest version of LinkedIn APIs for .NET in your Visual Studio project is through Nuget. From the Nuget's Package Manager Console in your Visual Studio .NET, simply execute the following command:

```
Install-Package LinkedIn
```

Go to http://www.nuget.org/packages/LinkedIn for more information about the Nuget package.

<h2>Configure LinkedIn Options in the Startup Class</h2>

In the <strong>ConfigureAuth</strong> method of the <strong>Startup</strong> partial class in your web project, you need to tell Owin that you want to access a LinkedIn user's access token once the user is successfully authenticated with LinkedIn. You can achieve this by the following code:

```
public void ConfigureAuth(IAppBuilder app)
{
  '...
  var linkedInOptions = new LinkedInAuthenticationOptions();

  linkedInOptions.ClientId = "Your LinkedIn API Key";
  linkedInOptions.ClientSecret = "Your LinkedIn Secret Key";
  
  linkedInOptions.Scope.Add("r_fullprofile");
  
  linkedInOptions.Provider = new LinkedInAuthenticationProvider()
  {
    OnAuthenticated = async context =>
    {
      context.Identity.AddClaim(new System.Security.Claims.Claim("LinkedIn_AccessToken", context.AccessToken));
    }
  };

  linkedInOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;

  app.UseLinkedInAuthentication(linkedInOptions);
  '...
}
```
<h2>Store LinkedIn Access Token in the Identity Database</h2>

One last step before you would be ready to make API calls. You need to store user's access token in your Identity database. You need to do this when the external login provider (in this case LinkedIn) redirects to your application after successful login. Typically its the <strong>ExternalLoginCallback</strong> action method of the <strong>AccountController</strong> class. You can store access token in the Identity database using the following code:
```
var claimsIdentity = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
            
if (claimsIdentity != null)
{
  var currentClaims = await UserManager.GetClaimsAsync(user.Id);
  var accessToken = claimsIdentity.FindAll(loginProvider + "_AccessToken").First();
                
  if (currentClaims.Count() <= 0)
  {
    await UserManager.AddClaimAsync(user.Id, accessToken);
  }
}
```
The above code store access token in the database only if it doesn't exist for the user.

<h2>Get the Profile Information</h3>

If you have followed all the above steps, it is just a matter of few lines of code to make API calls such as retrieving user's LinkedIn profile information. You need to first import two namespaces as follows:

```
'...
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LinkedIn.Api.Client.Owin;
using LinkedIn.Api.Client.Owin.Profiles;
'...
```

Next, you need to get the LinkedIn access token:

```
var am = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
var user = am.FindById(this.User.Identity.GetUserId());
var claim = user.Claims.ToList().Where(m => m.ClaimType == "LinkedIn_AccessToken").SingleOrDefault();
```            

Once you have the access token, you can use the following code to get the LinkedIn user's full profile:
```
var client = new LinkedInApiClient(HttpContext.GetOwinContext().Request, claim.ClaimValue);
var profileApi = new LinkedInProfileApi(client);
var userProfile = await profileApi.GetFullProfileAsync();
```

If you are just interested in the basic profile, you can call the <strong>GetBasicProfileAsync()</strong> method instead. The <strong>GetBasicProfileAsync()</strong> method returns an instance of the <strong>LinkedInBasicProfile</strong> class while <strong>GetFullProfileAsync()</strong> returns an instance of the <strong>LinkedInFullProfile</strong> class. The <strong>LinkedInFullProfile</strong> class inherits from the <strong>LinkedInBasicProfile</strong> class.

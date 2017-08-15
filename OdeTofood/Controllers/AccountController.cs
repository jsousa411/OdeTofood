using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OdeTofood.Entities;
using OdeTofood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeTofood.Controllers
{
    public class AccountController:  Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _singInManager;


        /// <summary>
        ///this can be done by validating directly with the database
        ///but by using this method we can create the user in 
        ///a safer manner
        /// </summary>
        /// <param name="userManager"></param>
        public AccountController(UserManager<User> userManager, 
                                 SignInManager<User> signInManager)
        {

                _userManager = userManager;
                _singInManager = signInManager;

        }
       
        [HttpGet]
        public IActionResult Register()
        {

            return View();

        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
              

            if (ModelState.IsValid)
            {

                var user = new User { UserName = model.Username };
                //since this is an async method
                //we need teh task<>
                var createResult = await _userManager.CreateAsync(user, model.Password);

                //check for valid sing in and
                //user creation
                if (createResult.Succeeded)
                {

                    //setting to false, user will need to sign in
                    //every time the user logs out....impersistent cookie 
                    //created
                    await _singInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                else//user failed to be created
                {

                    foreach(var error in createResult.Errors)
                    {
                        //pass in blank string to avoid displaying
                        //specific key error only
                        //"" string allows all errors to be displayed
                        //which in this case are ModelONly validation 
                        //warnings
                        ModelState.AddModelError("",error.Description);

                    }

                }

            }

            return View();


        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            await _singInManager.SignOutAsync();

            //take user to the main login page
            return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();//view for user to login
        }
        
        //validate anti forgery token
        [HttpPost, ValidateAntiForgeryToken]

        //all of the form input will be placed into the input model
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //validate mode state is valid

            if (ModelState.IsValid)
            {

                //"true" as last parameters locks out users if this attempt fails
                var loginResult = await _singInManager.PasswordSignInAsync(
                                        model.Username, 
                                        model.Password, 
                                        model.RememberMe, false);

                if (loginResult.Succeeded)
                {

                    //check if local url is local
                    //ensure open redirect does not occur
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {

                        return Redirect(model.ReturnUrl);
                        
                    }
                    else
                    {
                        //if there is something wrong,
                        //just send user to home page
                        return RedirectToAction("Index", "Home");

                    }
                }
            }

            //ensure some error shows on validation summary
            //"" as first parameter entails the error is
            //not associated with any property
            //second parameter just give generic info due to security reasons
            ModelState.AddModelError("", "Could Not Loing");
            return View(model);
        }
    }
}

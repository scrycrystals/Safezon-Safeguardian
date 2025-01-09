
namespace app2;
using app2.Models;
using app2.ViewModels;
using System;
using System.Text.RegularExpressions;

public partial class Register : ContentPage
{

    private RegisterViewModel _viewModel;
    
    public Register()
    {
        InitializeComponent();
        _viewModel = new RegisterViewModel();
    }


    private void Registerbtn_Clicked(object sender, EventArgs e)
    {
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        bool validemail = Regex.IsMatch(txtEmail.Text, regex);
        if (!validemail)
        {
            DisplayAlert("Error", "Email format is not correct!", "OK");
            return;
        }

        var newUser = new RegisterModel
        {
            Username = txtUsername.Text,
            Email = txtEmail.Text,
            Password = txtPassword.Text,
            PhoneNumber = txtPhoneNumber.Text
        };

        if (string.IsNullOrWhiteSpace(newUser.Username) ||
            string.IsNullOrWhiteSpace(newUser.Email) ||
            string.IsNullOrWhiteSpace(newUser.Password) ||
            string.IsNullOrWhiteSpace(newUser.PhoneNumber))
        {
            DisplayAlert("Error", "Please fill all fields.", "OK");
            return;
        }

        bool isRegistered = _viewModel.RegisterUser(newUser);

        if (isRegistered)
        {
            DisplayAlert("Success", "User registered successfully.", "OK");
            Navigation.PushAsync(new Dashboard1());
        }
        else
        {
            DisplayAlert("Error", "Registration failed. Please try again.", "OK");
        }
    }
}



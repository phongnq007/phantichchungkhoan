import { TextField } from "@material-ui/core";
import React, { FormEvent, useState } from "react";
import { connect } from "react-redux";
import { doLogin } from "../../redux/actions/loginAction";

interface LoginModel {
    email: string;
    password: string;
    rememberMe: boolean;
}

const LoginPage = (props: any) => {
    const [loginInput, setLoginInput] = useState<LoginModel>({
        email: "", password: "", rememberMe: false
    });

    const emailChange = (e: any) => {
        const nextState = { ...loginInput };
        nextState.email = e.target.value;
        setLoginInput(nextState);
    };

    const passChange = (e: any) => {
        const nextState = { ...loginInput };
        nextState.password = e.target.value;
        setLoginInput(nextState);
    };

    const rememberChange = (e: any) => {
        const nextState = { ...loginInput };
        nextState.rememberMe = e.target.checked;
        setLoginInput(nextState);
    };

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        //alert("test login");
        props.doLogin(loginInput);
    };

    return (
        <>
            <h1>Login</h1>
            <div className="row">
                <div className="col-md-4">
                    <section>
                        <form id="Login_form" onSubmit={handleSubmit}>
                            <h4>Use a local account to log in.</h4>
                            <hr />
                            <div asp-validation-summary="All" className="text-danger">
                                {props.loginState.message && <span>{props.loginState.message[0]}</span>}
                            </div>
                            <div className="form-group">
                                <label htmlFor="loginInput_email">Email</label>
                                <input id="loginInput_email" type="text" value={loginInput.email} onChange={emailChange} className="form-control" />
                                <span asp-validation-for="Input.Email" className="text-danger"></span>
                            </div>
                            <div className="form-group">
                                <label htmlFor="loginInput_password">Password</label>
                                <input id="loginInput_password" type="password" value={loginInput.password} onChange={passChange} className="form-control" />
                                <span asp-validation-for="Input.Password" className="text-danger"></span>
                            </div>
                            <div className="form-group">
                                <div className="checkbox">
                                    <label htmlFor="loginInput_rememberMe">
                                        <input type="checkbox" id="loginInput_rememberMe" checked={loginInput.rememberMe} onChange={rememberChange} />
                            Remember me?
                        </label>
                                </div>
                            </div>
                            <div className="form-group">
                                <button type="submit" className="btn btn-primary">Log in</button>
                            </div>
                            <div className="form-group">
                                <p>
                                    <a id="forgot-password" asp-page="./ForgotPassword">Forgot your password?</a>
                                </p>
                                <p>
                                    <a>Register as a new user</a>
                                </p>
                            </div>
                        </form>
                    </section>
                </div>
            </div>
        </>
    );
};

const mapStatesToProps = (state: any) => ({
    loginState: state.login
});

const mapActionsToProps = {
    doLogin
};
export default connect(mapStatesToProps, mapActionsToProps)(LoginPage);
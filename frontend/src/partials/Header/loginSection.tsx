import React from "react";
import { connect, useDispatch } from "react-redux";
import { NavLink } from "react-router-dom";
import { doLogout } from "../../redux/actions/loginAction";

const LoginSection = (props: any) => {

    return (
        <ul className="navbar-nav">
            {props.user.authenticated && <>
                <li className="nav-item">
                    <NavLink to="/manage-account" className="nav-link text-dark" title="Manage">Hello {props.user.userEmail}!</NavLink>
                </li>
                <li className="nav-item">
                    <button type="button" className="nav-link btn btn-link text-dark" onClick={props.doLogout}>Logout</button>
                </li>
            </>}
            {!props.user.authenticated && <>
                <li className="nav-item">
                    <NavLink to="/register" className="nav-link text-dark"></NavLink>
                </li>
                <li className="nav-item">
                    <NavLink to="/login" className="nav-link text-dark"></NavLink>
                </li>
            </>}
        </ul>
    );
};

const mapStateToProps = (state: any) => ({
    user: state.user
});

const mapActionToProps = {
    doLogout
};

export default connect(mapStateToProps, mapActionToProps)(LoginSection);
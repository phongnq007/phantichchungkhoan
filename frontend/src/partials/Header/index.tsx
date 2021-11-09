import React from "react";
import { Link, NavLink } from "react-router-dom";
import LoginSection from "./loginSection";

const Header = () => {
    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <Link className="navbar-brand" to="/Home">PhanTichChungKhoan.WebApp</Link>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <LoginSection />
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <NavLink className="nav-link text-dark" to="/">Home</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-dark" to="/buying-range">Portfolio</NavLink>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
};

export default Header;
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import {
  BrowserRouter as Router,
  Switch
} from "react-router-dom";
import Homepage from './components/HomePage';
import LoginPage from './components/Login';
import NotFoundPage from './components/NotFound';
import Footer from './partials/Footer';
import Header from './partials/Header';
import { CheckAuthentication } from "./utils/CheckAuthentication";
import GuestRoute from './utils/GuestRoute';
import PrivateRoute from './utils/PrivateRoute';
import ListBuyingRange from "./components/Portfolio/ListBuyingRange";
import CreateBuyingRange from './components/Portfolio/CreateBuyingRange';

function App() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(CheckAuthentication);
  }, [dispatch]);

  return (
    <Router>
      <Header />
      <Switch>
        <GuestRoute exact path="/" component={Homepage} />
        <GuestRoute exact path="/login" component={LoginPage} />

        <PrivateRoute exact path="/buying-range" component={ListBuyingRange} />
        <PrivateRoute exact path="/create-buying-range" component={CreateBuyingRange} />

        <GuestRoute exact component={NotFoundPage} />
      </Switch>
      <Footer />
    </Router>
  );
}

export default App;

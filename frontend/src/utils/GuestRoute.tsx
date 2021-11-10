import React, { Component } from 'react';
import { Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';

const GuestRoute = ({component: Component, authenticated, ...rest}: any) => {
 return <Route {...rest} render={routeProps => (authenticated ? <Redirect to='/' /> : <Component {...routeProps} />)}/>
};

const mapStateToProps = (state:any) => ({
 authenticated: state.user.authenticated
});
 
 export default connect(mapStateToProps)(GuestRoute)

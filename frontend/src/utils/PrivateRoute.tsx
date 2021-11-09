import React, { Component } from 'react';
import { Route, Redirect, RouteProps } from 'react-router-dom';
import { connect } from 'react-redux';

// interface PrivateRouteProps extends RouteProps {
//  component: Component;
//  authenticated: boolean;
// }

const PrivateRoute = ({ component: Component, authenticated, ...rest }:any) => (
    <Route
    {...rest}
    render={(props) =>
    authenticated ?
    <Component {...props} /> :
    <Redirect to='/login' />}/>
   );
   

// const PrivateRoute = (props: PrivateRouteProps) => {
//     return <Route {...props.rest} render={(routeProps) => (props.authenticated ? <Component {...routeProps} /> : <Redirect to='/login' />)} />
// };

const mapStateToProps = (state:any) => ({
 authenticated: state.user.authenticated
});
 
 export default connect(mapStateToProps)(PrivateRoute)

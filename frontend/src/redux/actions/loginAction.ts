import httpClient from '../../utils/httpClient';
import { Set_Authenticated_User, Request_Login_Start, Request_Login_Finished, Request_Login_Failed, Set_UnAuthenticated_User } from '../actionTypes';
import { AuthenticationAction } from "../reducers/authReducer";
import { LoginAction } from "../reducers/loginReducer";
import { UserInfo } from "../reducers/authReducer";
import jwtDecode from 'jwt-decode';

interface LoginData {
    email: string,
    password: string,
    rememberMe: boolean
}

export const doLogin = (loginData: LoginData) => {
    return async (dispatch: any) => {
        try {
            const response = await httpClient.post("/authmanagement/login", loginData);

            if (response.data.result) {
                const actionType: AuthenticationAction = {
                    type: Set_Authenticated_User,
                    payload: {
                        userEmail: response.data.userEmail,
                        userId: response.data.userId
                    }
                };

                const authToken = `Bearer ${response.data.token}`;
                httpClient.defaults.headers.common['Authorization'] = authToken;
                localStorage.setItem("token", authToken);

                dispatch(actionType);
            }
            else {
                const actionType: LoginAction = {
                    type: Request_Login_Failed,
                    message: response.data.errors
                };

                dispatch(actionType);
            }

        } catch (error) {
            const actionType: LoginAction = {
                type: Request_Login_Failed,
                message: [error.message]
            };

            dispatch(actionType);
        }
    };
};

export const doLogout = () => {
    return (dispatch: any) => {
        try {
            localStorage.removeItem('token');

            const actionType: AuthenticationAction = {
                type: Set_UnAuthenticated_User,
                payload: null
            };

            dispatch(actionType);
            window.location.href = '/login';
        } catch (error) {
            console.log("doLogout: " + error.message);
        }
    };
};

export const getUserInfo = (authToken: string) => {
    return async (dispatch: any) => {
        const decodedToken: any = jwtDecode(authToken);
        const userInfo: UserInfo = { userEmail: decodedToken.email, userId: decodedToken.Id };

        const actionType: AuthenticationAction = {
            type: Set_Authenticated_User,
            payload: userInfo
        };

        dispatch(actionType);
    };
};

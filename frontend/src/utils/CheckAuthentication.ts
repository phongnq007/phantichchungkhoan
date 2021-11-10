import jwtDecode from 'jwt-decode';
import { doLogout, getUserInfo } from "../redux/actions/loginAction";
import httpClient from './httpClient';

export const CheckAuthentication = (dispatch: any) => {
    const authToken = localStorage.token;

    if (authToken) {
        const decodedToken: any = jwtDecode(authToken);
        console.log(`expired date: ${decodedToken.exp}`);

        if (decodedToken.exp * 1000 < Date.now()) {
            dispatch(doLogout());
        }
        else {
            httpClient.defaults.headers.common['Authorization'] = authToken;
            dispatch(getUserInfo(authToken));
        }
    }
}

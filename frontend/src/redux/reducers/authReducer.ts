import { Set_Authenticated_User, Set_UnAuthenticated_User } from "../actionTypes";

export interface UserInfo {
    userEmail: string;
    userId: string;
}

interface AuthenticationState extends UserInfo {
    authenticated: boolean;
}

export interface AuthenticationAction {
    type: string;
    payload: UserInfo | null;
}

const initState: AuthenticationState = {
    authenticated: false,
    userEmail: "",
    userId: ""
};

const authReducer = (state = initState, action: AuthenticationAction): AuthenticationState => {
    switch (action.type) {
        case Set_Authenticated_User:
            return {
                authenticated: true,
                userEmail: action.payload!.userEmail,
                userId: action.payload!.userId
            };
        case Set_UnAuthenticated_User:
            return initState;
        default:
            return { ...state };
    }
};

export default authReducer;
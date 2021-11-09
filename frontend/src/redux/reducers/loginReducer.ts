import { stat } from 'node:fs';
import {Request_Login_Start, Request_Login_Finished, Request_Login_Failed} from '../actionTypes';

interface LoginState {
    message: Array<string>
}

export interface LoginAction {
    type: string,
    message: Array<string>
}

const initState: LoginState = {
    message: []
};


const loginReducer = (state = initState, action: LoginAction): LoginState => {
switch (action.type) {
    case Request_Login_Failed:
        return {
            message: action.message
        };

    default:
        return {...state};
}
};

export default loginReducer;
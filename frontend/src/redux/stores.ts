import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { composeWithDevTools } from 'redux-devtools-extension';

import { combineReducers } from "redux";

import loginReducer from "./reducers/loginReducer";
import authReducer from "./reducers/authReducer";

const rootReducer = combineReducers({
    login: loginReducer,
    user: authReducer
});

const configureStore = (initialState = {}) => {
  const composeEnhancers = composeWithDevTools(applyMiddleware(thunk));

  const store = createStore(rootReducer, initialState, composeEnhancers);

  // if (process.env.NODE_ENV !== 'production' && module.hot) {
  //   module.hot.accept('../', () => store.replaceReducer(rootReducer));
  // }

  return store;
}

export default configureStore;

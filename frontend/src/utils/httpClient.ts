import axios from 'axios';

// const httpClient = () => {
//     const authToken = localStorage.token;
//     return axios.create({
//         baseURL: 'http://localhost:3000/api',
//         headers: {
//             'Authorization': authToken
//         }
//     });
// };

const httpClient = axios.create({
    baseURL: 'http://localhost:5000/api'
    // headers: {
    //     'Authorization': localStorage.token
    // }
});

export default httpClient;
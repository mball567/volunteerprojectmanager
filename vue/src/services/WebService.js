import axios from "axios";

const http = axios.create({
    baseURL: process.env.VUE_APP_REMOTE_API
});

export default {
    getProfileInfo(userId){
        // if(user.isOrganization){
        //     let url = `/organizations/${userId}`
        // }
            
        let url = `/profiles/${userId}`
        
        return http.get(url);
    }
}
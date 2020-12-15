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
    },
    getTeamInfo(teamId){
        let url = `/teams/${teamId}`
        return http.get(url);
    },
    getProjectInfo(projId){
        let url = `/projects/${projId}`
        return http.get(url);
    },
    createProfile(profile){
        let url = '/profiles'

        return http.post(url, profile);
    },

    createTeam(team){
        let url = '/teams'

        return http.post(url, team);
    },

    createProject(project){
        let url = '/projects'

        return http.post(url, project);
    },
    getCauses(){
        let url = '/causes'

        return http.get(url);
    },
}
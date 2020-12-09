<template>
  <div>
    <h1>{{profile.firstName}} {{profile.lastName}}</h1>
    <h1>{{profile.profId}}</h1>
    <h1>{{profile.userId}}</h1>
    <h1>{{profile.bio}}</h1>
    <h1>{{profile.profileZip}}</h1>
    <h1>{{profile.profileCity}}</h1>    
    <h1>{{profile.profileState}}</h1>
    <h1>{{profile.profileContactEmail}}</h1>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'
export default {
name: "profile",
data() {
    return {
        profile: {
            profId: 0,
            userId: 0,
            firstName: "",
            lastName: "",
            profileImg: "",
            bio: "",
            profileZip: 0,
            profileCity: "",
            profileState: "",
            profileContactEmail: "",
        }
    };
},
methods: {
    getProfileInfo(userId){
        WebService.getProfileInfo(userId)
        .then( response => {
            this.profile = response.data;
        });
    }
},
created() {
    let userId = this.$route.params.userId;
    this.getProfileInfo(userId);
}
}
</script>

<style>

</style>
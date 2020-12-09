<template>
  <div>
    <h1>{{profile.firstName}} {{profile.lastName}}</h1>
    <p>Profile ID: {{profile.profileId}}</p>
    <p>User ID: {{profile.userId}}</p>
    <p>Bio: {{profile.bio}}</p>
     <p>Zip: {{profile.profZip}}</p>
     <p>City: {{profile.profCity}}</p>  
     <p>State: {{profile.profState}}</p>
    <p>Email: {{profile.profContactEmail}}</p>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'
export default {
name: "profile",
data() {
    return {
        profile: {
            profileId: 0,
            userId: 0,
            firstName: "",
            lastName: "",
            profImg: "",
            bio: "",
            profZip: 0,
            profCity: "",
            profState: "",
            profContactEmail: "",
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
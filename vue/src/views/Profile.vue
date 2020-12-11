<template>
  <div>
    <div v-show="isOrg === false">
      <h1>{{ profile.firstName }} {{ profile.lastName }}</h1>
      <p>Profile ID: {{ profile.profileId }}</p>
      <p>User ID: {{ profile.userId }}</p>
      <p>Bio: {{ profile.bio }}</p>
      <p>Zip: {{ profile.profZip }}</p>
      <p>City: {{ profile.profCity }}</p>
      <p>State: {{ profile.profState }}</p>
      <p>Email: {{ profile.profContactEmail }}</p>
    </div>
    <div v-show="isOrg === true">
      <h1>{{ organization.orgName }}</h1>
      <p>Organization ID: {{ organization.orgId }}</p>
      <p>Bio: {{ organization.orgBio }}</p>
      <p>Zip: {{ organization.orgZip }}</p>
      <p>City: {{ organization.orgCity }}</p>
      <p>State: {{ organization.orgState }}</p>
      <p>Email: {{ organization.orgContactEmail }}</p>
    </div>
  </div>
</template>

<script>
import WebService from "../services/WebService.js";
export default {
  name: "profile",
  data() {
    return {
      profile: {
        profileId: 0,
        userId: 0,
        firstName: "",
        lastName: "",
        profileImage: "",
        bio: "",
        profZip: 0,
        profCity: "",
        profState: "",
        profContactEmail: "",
      },
      organization: {
        orgId: "",
        userId: 0,
        orgName: "",
        orgImage: "",
        orgBio: "",
        orgZip: 0,
        orgCity: "",
        orgState: "",
        orgContactEmail: "",
      },
      isOrg: false,
    };
  },
  methods: {
    getProfileInfo(userId) {
      WebService.getProfileInfo(userId).then((response) => {
        this.profile = response.data;
      });
    },
  },
  created() {
    // let userId = this.$route.params.userId;
    // this.getProfileInfo(userId);
    let userId = this.$route.params.userId;
    if (userId === "my") {
        if(this.$store.state.user.profile){
            this.profile = this.$store.state.user.profile;
        }
        else{
            this.organization = this.$store.state.user.organization;
            this.isOrg = true;  
        }
    } else {
      this.getProfileInfo(userId);
    }
  },
};
</script>

<style>
</style>
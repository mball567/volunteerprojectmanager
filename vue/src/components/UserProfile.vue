<template>
  <div>
      <form>
          <label for="firstName">First Name: </label>
          <input id="firstName" type="text" v-model="profile.firstName" required/>
          
          <label for="lastName">Last Name: </label>
          <input id="lastName" type="text" v-model="profile.lastName" required/>
          
          <label for="bio">User Bio: </label>
          <input id="bio" type="text" v-model="profile.bio" required/>
          
          <label for="zipCode">ZIP Code (5 digits):</label>
          <input type="text" id="zipCode" pattern="[0-9]{5}" title="Five digit zip code" v-model="profile.profZip" required/>

          <label for="city">City: </label>
          <input id="city" type="text" v-model="profile.profCity" required/>
          
          <label for="state">State: </label>
          <input id="state" type="text" v-model="profile.profState" required/>

          <label for="e-mail">E-Mail Address: </label>
          <input type="email" v-model="profile.profContactEmail" required/>
        </form>
        <label for="addProfile">Please confirm this information is correct: </label>
        <input type="checkbox" id="addProfile" v-model="checked" v-on:change="addUserProfile" required/>
        
  </div>
</template>

<script>

export default {
    name: 'user-profile',
    props: {
        
    },
    data(){
        return {
            profile: {
                firstName: "",
                lastName: "", 
                bio: "", 
                profileImage: "",
                profZip: 0,
                profCity: "",
                profState: "",
                profContactEmail: ""
            },
            checked: false,
        };
    },
    methods: {
        addUserProfile(){
            if (this.checked === true){
            this.$store.commit('ADD_PROFILE', this.profile);
            }
            else {
            this.profile = {firstName: "", lastName: "", bio: "", profileImage: "", profZip: 0, profCity: "", profState: "", profContactEmail: ""};
            }
            
        }
    },
}
</script>

<style scoped>

</style>
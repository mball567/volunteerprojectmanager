<template>
  <div class="container">
    <body>
      

    <div v-show="isOrg === false">
     
      <main id="main-grid">
        <nav id="site-nav">
          <ul>
            <li><a href="#">My projects</a></li>&nbsp;|&nbsp;
            <li><a href="#">My teams</a></li>&nbsp;|&nbsp;
            <li><a href="#">Akt with others</a></li>&nbsp;|&nbsp;
            <li><a href="#">Logout</a></li>
          </ul>
        </nav>
     

    <aside>
      <img id="userImage" :src="profile.profileImage" />
      <h2>{{ profile.firstName }} {{ profile.lastName }}</h2>
          <p>Email: {{ profile.profContactEmail }}</p>
          <p>Currently akting in: {{ profile.profCity }}, {{ profile.profState }}</p>
    </aside>


    <p id="bio">{{ profile.bio }}</p>
    </main>
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
    </body>
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

/* color
--light-cornflower-blue: #8ecae6ff;
--blue-green: #219ebcff;
--prussian-blue: #023047ff;
--honey-yellow: #ffb703ff;
--orange: #fb8500ff;
*/

@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap');

#main-grid {
display: grid;
grid-template-columns: 1fr 1fr 1fr;
grid-template-areas:
  "nav nav nav"
  "aside bio bio"
  "aside causes causes"
}

body {
  background-color: #023047ff;
  font-family: 'Montserrat', sans-serif;
  font-weight: 400;
}


main p#bio {
grid-area: bio;
color: white;
}

main nav#site-nav {
  grid-area: nav;
  width: 100%;
  text-align: right;
}

main nav#site-nav ul {
  display: flex;
  align-items: stretch;
  justify-content: space-between;
  }

aside {
  color:white;
  grid-area: aside;
  height: 100vh;

}

nav#site-nav li {
  list-style-type: none;
  display: inline;
}

nav#site-nav li a {
  list-style-type: none;
  color:#ffb703ff;
  padding: 20px;
  text-decoration: none;
  font-weight: bold;
  
}

nav#site-nav li a:hover {
  color: #219ebcff;
}

img#userImage{
  border-radius: 50%;
  width: 200px;
  height: 200px;
}

</style>
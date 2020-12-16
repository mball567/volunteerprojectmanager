<template>
  <div class="container">
    <body>
      <div v-show="isOrg === false">
        <main id="main-grid">
          <nav id="site-nav">
            <ul>
              <li><a href="#">My projects</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">My teams</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">Akt with others</a></li>
              &nbsp;|&nbsp;
              <li><router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link></li>
              
              
            </ul>
          </nav>

          <aside>
            <img id="userImage" :src="profile.profileImage" />
            <h2>{{ profile.firstName }} {{ profile.lastName }}</h2>
            <p>Email: {{ profile.profContactEmail }}</p>
            <p>
              Currently akting in: {{ profile.profCity }},
              {{ profile.profState }}
            </p>
          </aside>

          <div id="bioArea">
          <h2>Bio:</h2>
          <p id="bio">{{ profile.bio }}</p>
          </div>

                <div id="causes">
                  <h2>Causes:</h2>
            <ul>
              <li v-for="causeName in profile.profCauseNames" v-bind:key="causeName">{{causeName}}</li>
            </ul>
          </div>

        </main>
      </div>

      <div v-show="isOrg === true">
        <main id="main-grid">
          <nav id="site-nav">
            <ul>
              <li><a href="#">My projects</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">My teams</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">Akt with others</a></li>
              &nbsp;|&nbsp;
              <li id="logout"><router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link></li>
            </ul>
          </nav>

          <aside>
            <img id="userImage" :src="organization.orgImage" />
            <h2>{{ organization.orgName }}</h2>
            <p>Email: {{ organization.orgContactEmail }}</p>
            <p>
              Currently akting in: {{ organization.orgCity }},
              {{ organization.orgState }}
            </p>
          </aside>

          <div id="bioArea">
          <h2>Bio:</h2>
          <p id="bio">{{ organization.orgBio }}</p>
          </div>

          <div id="causes">
            <h2>Causes:</h2>
            <ul>
              <li v-for="causeName in organization.orgCauseNames" v-bind:key="causeName">{{causeName}}</li>
            </ul>
          </div>

        </main>
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
        profCauseNames:[],
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
        orgCauseNames: [],
      },
      lookupUser: {
        userId: 0,
        username: "",
        passwordHash: "",
        salt: "",
        role: "",
        isOrganization: false,
      },
      isOrg: false,
    };
  },
  methods: {
    getUserInfo(userId) {
        WebService.getUserInfo(userId).then ((response) => {
          this.lookupUser = response.data;

          if(this.lookupUser.isOrganization === true){
          WebService.getOrgInfo(userId).then((response) => {
          this.organization = response.data;
          this.isOrg = true;
          });
          }
          else{
          WebService.getProfileInfo(userId).then((response) => {
          this.profile = response.data;
          });
          }
          });
    },
  },
  created() {
    // let userId = this.$route.params.userId;
    // this.getProfileInfo(userId);

    let userId = this.$route.params.userId;
    if (userId === "my") {
      if (this.$store.state.user.profile) {
        this.profile = this.$store.state.user.profile;
      } else {
        this.organization = this.$store.state.user.organization;
        this.isOrg = true;
      }
    } else {
      this.getUserInfo(userId);
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

@import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap");

#main-grid {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:
    "nav nav nav"
    "aside bio bio"
    "aside causes causes";
}

body {
  background-color: #023047ff;
  font-family: "Montserrat", sans-serif;
  font-weight: 400;
}

main div#bioArea {
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
  /* justify-content: space-evenly;
  text-align: center;*/
  background-color: #e5e5e5ff;
  padding-block: .5em;
}

aside {
  color: white;
  grid-area: aside;
  height: 100vh;
  padding-left: 1em;
  padding-right: 2em;
}

nav#site-nav li {
  list-style-type: none;
  display: inline;
  justify-content: space-evenly;
  text-align: center;

}
/* 
nav#site-nav li a {
  list-style-type: none;
  color: #ffb703ff;
  padding: 20px;
  text-decoration: none;
  font-weight: bold;
  
} */
/* nav#site-nav li#logout {
  list-style-type: none;
  color: #ffb703ff;
  padding: 20px;
  text-decoration: none;
  font-weight: bold;
  
} */


nav#site-nav li a:hover {
  color: #219ebcff;
}

img#userImage {
  border-radius: 50%;
  width: 200px;
  height: 200px;
}

div#causes {
  grid-area: causes;
  color: white;
}

div#causes ul {
  color: white;
  list-style-type: none;
  padding: 0;
}
</style>
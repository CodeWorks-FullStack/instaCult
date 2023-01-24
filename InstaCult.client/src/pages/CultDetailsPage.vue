<template>
  <div class="container-fluid text-warning">
    <div class="row" v-if="cult">
      <h2 class="col-12 text-center mb-2">
        {{ cult?.name }}
      </h2>
      <button v-if="account.id" class="btn btn-warning bg-primary rounded-pill" @click="joinCult">
        <i class="mdi mdi-plus"></i>
        join today!
      </button>
      <!-- SECTION about cult -->
      <div class="col-md-5">
        <div class="d-flex flex-column">
          our devoted leader
          <img style="height:150px; width:150px;" :src="cult.leader.picture" alt="">
          <b>{{ cult.leader.name }}</b>
        </div>
        <p>{{ cult.description }}</p>
      </div>


      <!-- SECTION members -->
      <div class="col-md-7">
        <div class="row">
          <!-- STUB cultist -->
          <div class="col-2 rounded" v-for="c in cultists">
            <img class="rounded-pill" style="height:60px; width:100px;" :src="c.picture" alt="">
            <p>{{ c.name }}</p>
            <button v-if="account.id == cult.leaderId" class="btn btn-outline-danger rounded-pill"
              title="give em the boot" @click="removeMember(c.cultMemberId)">👢</button>
          </div>
          <!--  -->
        </div>
      </div>

    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { cultMemberService } from '../services/CultMembersService.js'
import { useRoute } from 'vue-router';
export default {
  setup() {
    const route = useRoute()
    onMounted(() => {
      getCult()
      getCultMembers()
    })
    async function getCult() {
      try {
        await cultsService.getById(route.params.id)
      } catch (error) {
        Pop.error(error)
      }
    }
    async function getCultMembers() {
      try {
        await cultsService.getCultMembers(route.params.id)
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      account: computed(() => AppState.account),
      cult: computed(() => AppState.activeCult),
      cultists: computed(() => AppState.cultists),
      async joinCult() {
        try {
          // NOTE make sure you send an object to your server and not just a single value, your server has to know what that value represents.
          let cultMember = { cultId: route.params.id }
          await cultMemberService.joinCult(cultMember)
        } catch (error) {
          Pop.error(error, 'Joining Cult')
        }
      },
      async removeMember(cultMemberId) {
        try {
          await cultMemberService.removeMember(cultMemberId)
        } catch (error) {
          Pop.toast(error.data, 'error', 'center')
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>

</style>

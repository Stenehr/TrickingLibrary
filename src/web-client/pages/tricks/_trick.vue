<template>
    <div class="d-flex justify-center align-start">
        <div class="mx-5 mt-8" v-if="submissions">
            <div :key="x" v-for="x in 20">
                <div :key="indx" v-for="(s, indx) in submissions">
                    {{ s.id }} - {{ s.description }} trickId: {{ s.trickId }}
                    <div>
                        <video
                            controls
                            :src="`http://localhost:5000/api/videos/${s.video}`"
                        ></video>
                    </div>
                </div>
            </div>
        </div>
        <div class="mx-5 mt-8 sticky">Trick: {{ $route.params.trick }}</div>
    </div>
</template>

<script>
import { mapState } from "vuex";

export default {
    computed: mapState("submissions", ["submissions"]),

    async fetch() {
        const trickId = this.$route.params.trick;
        await this.$store.dispatch(
            "submissions/fetchSubmissionsForTrick",
            { trickId },
            { root: true }
        );
    }
};
</script>

<style scoped>
.sticky {
    position: sticky;
    position: -webkit-sticky;
    top: 64px;
}
</style>
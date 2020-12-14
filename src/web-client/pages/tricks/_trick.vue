<template>
    <div>
        <div>Trick: {{ $route.params.trick }}</div>
        <div v-if="submissions">
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

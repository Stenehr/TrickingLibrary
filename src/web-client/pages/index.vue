<template>
    <div>
        <v-file-input accept="video/*" @change="handleFile">
        </v-file-input>

        <div v-if="tricks">
            <p :key="indx" v-for="(t, indx) in tricks">
                {{ t.name }}
            </p>
        </div>
        <div>
            <v-text-field label="Trick name" v-model="trickName" />
            <v-btn @click="saveTrick">Save trick</v-btn>
        </div>
        <v-btn @click="resetTricks">ResetTricks</v-btn>
    </div>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";

export default {
    data: () => ({
        trickName: ""
    }),

    computed: {
        ...mapState("tricks", {
            tricks: state => state.tricks
        })
    },

    methods: {
        ...mapMutations("tricks", {
            resetTricks: "reset"
        }),

        ...mapActions("tricks", ["createTrick"]),

        async saveTrick() {
            await this.createTrick({ trick: { name: this.trickName } });
            this.trickName = "";
        },

        async handleFile(file) {
            if (!file) {
                return;
            }

            const formData = new FormData();
            formData.append("video", file);

            const result = await this.$axios.$post("http://localhost:5000/api/videos", formData);
            console.log(`Result: ${result}`);
        }
    }

    // async fetch() {
    //     await this.$store.dispatch("fetchMessage")
    // }
    // data: () => ({
    //     message: "TEST"
    // }),
    // async asyncData(payload) {
    //     const { data } = await Axios.get("http://localhost:5000/api/home");
    //     return {
    //         message: data
    //     };
    // }
};
</script>

<template>
    <div>
        <div v-if="tricks">
            <div :key="indx" v-for="(t, indx) in tricks">
                {{ t.name }}
                <div>
                    <video controls :src="`http://localhost:5000/api/videos/${t.video}`"></video>
                </div>
            </div>
        </div>


        <v-stepper v-model="step">
            <v-stepper-header>
                <v-stepper-step :complete="step > 1" step="1">
                    Upload Video
                </v-stepper-step>

                <v-divider></v-divider>

                <v-stepper-step :complete="step > 2" step="2">
                    Trick Information
                </v-stepper-step>

                <v-divider></v-divider>

                <v-stepper-step step="3">
                    Confirmation
                </v-stepper-step>
            </v-stepper-header>

            <v-stepper-items>
                <v-stepper-content step="1">
                    <div>
                        <v-file-input accept="video/*" @change="handleFile">
                        </v-file-input>
                    </div>
                </v-stepper-content>

                <v-stepper-content step="2">
                    <div>
                        <v-text-field label="Trick name" v-model="trickName" />
                        <v-btn @click="saveTrick">Save trick</v-btn>
                    </div>
                </v-stepper-content>

                <v-stepper-content step="3">
                    <div>
                        Success
                    </div>
                </v-stepper-content>
            </v-stepper-items>
        </v-stepper>
    </div>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";

export default {
    data: () => ({
        step: 1,
        trickName: ""
    }),

    computed: {
        ...mapState("tricks", ["tricks"]),
        ...mapState("videos", ["uploadPromise"]),
    },

    methods: {
        ...mapMutations("videos", {
            resetVideos: "reset"
        }),

        ...mapActions("tricks", ["createTrick"]),

        ...mapActions("videos", ["startVideoUpload"]),

        async handleFile(file) {
            if (!file) {
                return;
            }

            const form = new FormData();
            form.append("video", file);
            this.startVideoUpload({ form });
            this.step++;
        },

        async saveTrick() {
            if (!this.uploadPromise) {
                console.log("No video uploadPromise");
                return;
            }

            const video = await this.uploadPromise;
            await this.createTrick({ trick: { name: this.trickName, video } });
            this.trickName = "";
            this.resetVideos();
            this.step++;
        },
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

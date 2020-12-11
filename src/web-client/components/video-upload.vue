<template>
    <v-dialog :value="active" persistant>
        <template v-slot:activator>
            <v-btn depressed @click="toggleActivity">
                Upload
            </v-btn>
        </template>
        <v-stepper v-model="step">
            <v-stepper-header>
                <v-stepper-step :complete="step > 1" step="1">
                    Select Type
                </v-stepper-step>

                <v-divider></v-divider>

                <v-stepper-step :complete="step > 2" step="2">
                    Upload Video
                </v-stepper-step>

                <v-divider></v-divider>

                <v-stepper-step :complete="step > 3" step="3">
                    Trick Information
                </v-stepper-step>

                <v-divider></v-divider>

                <v-stepper-step step="4">
                    Confirmation
                </v-stepper-step>
            </v-stepper-header>

            <v-stepper-items>
                <v-stepper-content step="1">
                    <div class="d-flex flex-column align-center">
                        <v-btn @click="setType(uploadType.TRICK)">Trick</v-btn>
                        <v-btn
                            @click="setType(uploadType.SUBMISSION)"
                            class="mt-2"
                            >Submission</v-btn
                        >
                    </div>
                </v-stepper-content>

                <v-stepper-content step="2">
                    <div>
                        <v-file-input accept="video/*" @change="handleFile">
                        </v-file-input>
                    </div>
                </v-stepper-content>

                <v-stepper-content step="3">
                    <div>
                        <v-text-field label="Trick name" v-model="trickName" />
                        <v-btn @click="saveTrick">Save trick</v-btn>
                    </div>
                </v-stepper-content>

                <v-stepper-content step="4">
                    <div>
                        Review
                    </div>
                </v-stepper-content>
            </v-stepper-items>
        </v-stepper>
        <v-btn @click="toggleActivity" class="mt-1">
            Close
        </v-btn>
    </v-dialog>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";
import { UPLOAD_TYPE } from "../data/enums";

export default {
    data: () => ({
        trickName: ""
    }),

    computed: {
        ...mapState("videos-upload", ["uploadPromise", "active", "step"]),
        uploadType() {
            return UPLOAD_TYPE;
        }
    },

    methods: {
        ...mapActions("videos-upload", ["startVideoUpload", "createTrick"]),
        ...mapMutations("videos-upload", [
            "toggleActivity",
            "reset",
            "setType"
        ]),
        async handleFile(file) {
            if (!file) {
                return;
            }

            const form = new FormData();
            form.append("video", file);
            this.startVideoUpload({ form });
        },

        async saveTrick() {
            if (!this.uploadPromise) {
                console.log("No video uploadPromise");
                return;
            }

            const video = await this.uploadPromise;
            await this.createTrick({ trick: { name: this.trickName, video } });
            this.trickName = "";
            this.reset();
        }
    }
};
</script>

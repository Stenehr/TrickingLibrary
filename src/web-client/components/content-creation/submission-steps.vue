<template>
    <v-stepper v-model="step">
        <v-stepper-header>
            <v-stepper-step :complete="step > 1" step="1">
                Upload Video
            </v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step :complete="step > 2" step="2">
                Select Trick
            </v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step :complete="step > 3" step="3">
                Submission
            </v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step step="4">
                Review
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
                    <v-select :items="trickItems" v-model="form.trickId" label="Select Trick" />
                    <v-btn @click="step++">Next</v-btn>
                </div>
            </v-stepper-content>

            <v-stepper-content step="3">
                <div>
                    <v-text-field label="Description" v-model="form.description" />
                    <v-btn @click="step++">Next</v-btn>
                </div>
            </v-stepper-content>

            <v-stepper-content step="4">
                <div>
                    <v-btn @click="save">Save</v-btn>
                </div>
            </v-stepper-content>
        </v-stepper-items>
    </v-stepper>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";

const initState = () => ({
    form: {
        trickId: "",
        video: "",
        description: "",
    },
    step: 1
});

export default {
    name: "submisson-steps",

    data: initState,

    computed: {
        ...mapState("videos-upload", ["uploadPromise"]),
        ...mapState("tricks", ["tricks"]),
        trickItems() {
            return this.tricks.map(x => ({
                text: x.name,
                value: x.id
            }));
        }
    },

    methods: {
        ...mapActions("videos-upload", ["startVideoUpload"]),
        ...mapActions("submissions", ["createSubmission"]),
        ...mapMutations("videos-upload", [
            "toggleActivity",
            "reset",
            "setType",
            "incrementStep"
        ]),
        async handleFile(file) {
            if (!file) {
                return;
            }

            const form = new FormData();
            form.append("video", file);
            this.startVideoUpload({ form });
            step++;
        },

        async save() {
            if (!this.uploadPromise) {
                console.log("No video uploadPromise");
                return;
            }

            this.form.video = await this.uploadPromise;
            await this.createSubmission({ form: this.form });
            this.reset();
            Object.assign(this.$data, initState());
        }
    }
};
</script>

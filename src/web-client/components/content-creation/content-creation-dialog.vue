<template>
    <v-dialog :value="active" persistant>
        <template v-slot:activator="{ on }">
            <v-btn depressed v-on="on" @click="toggleActivity">
                UPLOAD
            </v-btn>
        </template>

        <trick-steps />

        <v-btn @click="toggleActivity" class="mt-1">
            Close
        </v-btn>
    </v-dialog>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";
import { UPLOAD_TYPE } from "../../data/enums";
import TrickSteps from "./trick-steps.vue";

export default {
    name: "content-creation-dialog",
    components: { TrickSteps },

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
            "setType",
            "incrementStep"
        ])
    }
};
</script>

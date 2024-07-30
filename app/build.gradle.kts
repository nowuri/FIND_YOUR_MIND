plugins {
    alias(libs.plugins.android.application)
    alias(libs.plugins.jetbrains.kotlin.android)
}

android {
    namespace = "com.example.find_your_mind"
    compileSdk = 34

    defaultConfig {
        applicationId = "com.example.find_your_mind"
        minSdk = 24
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
    kotlinOptions {
        jvmTarget = "1.8"
    }

    buildFeatures{
        viewBinding = true
    }
}

dependencies {
    implementation(libs.androidx.core.ktx)
    implementation(libs.androidx.appcompat)
    implementation(libs.material)
    implementation(libs.androidx.activity)
    implementation(libs.androidx.constraintlayout)
    testImplementation(libs.junit)
    androidTestImplementation(libs.androidx.junit)
    androidTestImplementation(libs.androidx.espresso.core)

    implementation ("com.pierfrancescosoffritti.androidyoutubeplayer:core:12.1.0")
    implementation("com.pierfrancescosoffritti.androidyoutubeplayer:chromecast-sender:0.28")
    implementation("com.squareup.okhttp3:okhttp:4.9.3")

    // CameraX 라이브러리 추가
    implementation("androidx.camera:camera-core:1.2.0")
    implementation("androidx.camera:camera-camera2:1.2.0")
    implementation("androidx.camera:camera-lifecycle:1.2.0")
    implementation("androidx.camera:camera-view:1.2.0")
    implementation("androidx.lifecycle:lifecycle-runtime-ktx:2.5.1")

    implementation(project(":unityLibrary"))
    implementation(fileTree(mapOf("dir" to file("C:/Unity_Projects/Python_Unity_ahlee/AndroidFiles/unityLibrary/libs"), "include" to listOf("*.jar"))))
}